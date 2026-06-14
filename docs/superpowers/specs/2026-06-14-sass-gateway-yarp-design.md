# SASS Gateway YARP Design

## Summary

Create a new `SASS.Gateway` service under `server/src/Services` that acts as the public HTTP entrypoint for backend traffic.

For v1, the gateway will:

- Proxy `/api/*` requests to `SASS.Chat`
- Proxy the current API documentation surfaces exposed by `SASS.Chat`
- Avoid owning authentication, authorization, aggregation, or business logic

This keeps the first gateway rollout intentionally small. `SASS.Chat` remains the application service and source of truth for endpoint behavior, while `SASS.Gateway` only handles request forwarding through YARP.

## Goals

- Introduce a dedicated edge service named `SASS.Gateway`
- Make the gateway the preferred public API entrypoint in local development
- Forward versioned API traffic to `SASS.Chat` without changing existing feature behavior
- Expose the existing documentation surface through the gateway
- Keep the implementation small enough to validate routing and hosting without broad architectural changes

## Non-Goals

- No auth termination or auth policy ownership in `SASS.Gateway`
- No request or response aggregation
- No backend feature migration from `SASS.Chat` into the gateway
- No SignalR/realtime proxying in this v1
- No custom app-level error model in the gateway
- No client migration work beyond what is necessary to point traffic at the gateway later

## Existing Context

- `server/src/Services` currently contains `SASS.Chat` only.
- `SASS.Chat` is an ASP.NET Core minimal API host with endpoint discovery, API versioning, authentication, authorization, and Scalar API documentation.
- `SASS.Chat` currently maps application endpoints through `app.MapEndpoints(apiVersionSet)` and realtime endpoints through `app.MapRealtimeEndpoints()`.
- `SASS.Chat` currently calls `app.UseApiDocumentation()`, which maps the existing API documentation surface.
- `SASS.Chat` local launch settings currently expose HTTP on `http://localhost:5146`.
- Protected behavior already lives in `SASS.Chat` endpoints through explicit `.RequireAuthorization()`.

## Recommended Approach

Use a thin YARP-based edge service that proxies only:

- `/api/{**catch-all}` to `SASS.Chat`
- the current documentation routes exposed by `SASS.Chat`

This is preferred over a broader catch-all proxy because it creates a clear responsibility boundary early:

- `SASS.Gateway` owns transport and routing only
- `SASS.Chat` owns endpoint behavior, auth rules, validation, and domain logic

It is also preferred over a more future-heavy multi-service gateway design because only one downstream service exists today, and the user explicitly wants a small v1.

## Architecture

### New Service

Add a new backend service project:

- `server/src/Services/SASS.Gateway`

This service should be a minimal ASP.NET Core host following the existing repo's backend conventions where practical, but it does not need CQRS feature slices because it is not an application API.

### Core Responsibility

`SASS.Gateway` should:

- Start an HTTP host
- Load reverse-proxy routes and clusters from configuration
- Forward matching requests to `SASS.Chat` using YARP

`SASS.Gateway` should not:

- Implement business endpoints
- Duplicate validation or authorization logic
- Translate downstream DTOs
- Add service orchestration behavior

## Routing Design

### API Routes

The gateway should proxy versioned API traffic using a route shape equivalent to:

- `/api/{**catch-all}` -> `SASS.Chat`

This keeps the public path stable while ensuring all existing `SASS.Chat` endpoints continue to behave as before.

### Documentation Routes

The gateway should also proxy the current API documentation surfaces exposed by `SASS.Chat`.

The implementation should map the actual documentation paths used by the current Scalar/OpenAPI setup rather than inventing new public paths in v1.

### Exclusions

The gateway should not proxy realtime endpoints in this first version unless later implementation evidence shows they are already required for basic app startup.

## Configuration Design

Gateway configuration should live in `SASS.Gateway` app configuration rather than code constants.

For v1, define:

- one YARP cluster representing `SASS.Chat`
- destination address pointing at the local `SASS.Chat` base URL
- route entries for `/api/{**catch-all}` and the documentation surfaces

This keeps environment-specific downstream addresses configurable and allows local URLs to change without code edits.

The configuration should be structured so additional routes or clusters can be added later without restructuring the service, but the initial config should remain focused on the single current downstream.

## Request Flow

For a normal API request:

1. A client sends the request to `SASS.Gateway`
2. The gateway matches a configured YARP route
3. The gateway forwards the request to `SASS.Chat`
4. `SASS.Chat` handles authentication, authorization, validation, endpoint execution, and response generation
5. The gateway returns the downstream response to the client

For a documentation request:

1. A client calls the documentation route on `SASS.Gateway`
2. The gateway forwards that request to `SASS.Chat`
3. `SASS.Chat` serves the current documentation response

This preserves a single ownership model for all application behavior while still introducing a distinct edge host.

## Error Handling

The gateway should stay passive in v1.

- Downstream application responses from `SASS.Chat` should pass through unchanged
- Existing problem details and status codes from `SASS.Chat` should remain the source of truth
- If `SASS.Chat` is unavailable, the gateway may surface the normal reverse-proxy failure behavior from YARP

No custom gateway exception-mapping layer is needed in this first version.

## Security Boundary

Authentication and authorization are explicitly out of scope for gateway ownership in v1.

That means:

- the gateway forwards identity-bearing requests as transport
- `SASS.Chat` remains responsible for auth middleware and endpoint authorization
- no edge-specific access policy is introduced yet

This is intentionally conservative and reduces the risk of behavior drift during the first gateway rollout.

## Local Development Shape

`SASS.Gateway` should be runnable as its own backend service in local development.

The expected local model is:

- `SASS.Chat` runs on its own port
- `SASS.Gateway` runs on its own port
- callers use `SASS.Gateway` as the preferred entrypoint for proxied API and documentation traffic

Direct access to `SASS.Chat` may still exist for local troubleshooting, but it is no longer the intended public entrypoint once the gateway is in place.

## Deliverables

- New `server/src/Services/SASS.Gateway` project
- YARP package and reverse-proxy registration
- Gateway startup code that loads reverse-proxy config and maps proxy endpoints
- Gateway configuration for one downstream `SASS.Chat` cluster
- Route entries for `/api/*` and current documentation surfaces
- Solution wiring so the new project can build and run with the repo
- Local launch configuration for the gateway

## Verification Strategy

Validation for this work should stay targeted:

- Build the new gateway project successfully
- Confirm the gateway starts with valid route and cluster configuration
- Manually verify that an `/api/...` request sent to the gateway reaches `SASS.Chat`
- Manually verify that the current documentation surface is reachable through the gateway
- Confirm direct `SASS.Chat` behavior remains unchanged aside from no longer being the preferred public entrypoint

If unrelated repo-wide failures exist, they should not be treated as regressions in the gateway work.

## Implementation Notes

- Keep the gateway host minimal and declarative
- Prefer configuration-driven YARP setup over hardcoded route logic
- Match the repo's existing ASP.NET Core hosting style where it makes sense, but do not force gateway code into application-feature patterns it does not need
- Preserve the current `SASS.Chat` behavior rather than refactoring chat service startup during this change
- If documentation route details are not obvious from the current setup, confirm the actual paths during implementation and proxy those exact surfaces
