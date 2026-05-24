import { useNavigate } from "@tanstack/react-router";
import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import {
  Button,
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
  Field,
  FieldError,
  FieldLabel,
  Input,
  Spinner,
  Textarea,
} from "@/features/shared";
import { createProjectOptions } from "../create-project-options.ts";
import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";
import {
  type CreateProjectFormValues,
  createProjectSchema,
} from "../validators.ts";
import { Plus } from "lucide-react";
import { useState } from "react";
import { normalizeApiError } from "@/lib";

export function CreateProjectButton() {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);

  const form = useForm<CreateProjectFormValues>({
    resolver: zodResolver(createProjectSchema),
    defaultValues: {
      code: "",
      title: "",
      description: "",
    },
  });

  const { mutate, isPending } = useMutation({
    ...createProjectOptions(),
    onSuccess: async (response) => {
      setOpen(false);

      toast.success(
        `Create Project "${form.getValues("title")}" successfully`,
        {
          position: "bottom-right",
        },
      );

      form.reset();

      await navigate({
        to: "/projects/$projectId",
        params: { projectId: response.id },
      });
    },
    onError: (error) => {
      const normalizedError = normalizeApiError(error);
      toast.error(normalizedError.detail, { position: "bottom-right" });
    },
  });

  function onSubmit(values: CreateProjectFormValues) {
    mutate(values);
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button
          type="button"
          variant="default"
          className="w-full cursor-pointer hover:bg-accent hover:text-accent-foreground"
        >
          <Plus className="size-4" />
          Create project
        </Button>
      </DialogTrigger>

      <DialogContent className="sm:max-w-sm">
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
          <DialogHeader>
            <DialogTitle>Create conversation</DialogTitle>
            <DialogDescription>
              Create new conversation to get started. You can always change the
              name later.
            </DialogDescription>
          </DialogHeader>

          <Controller
            control={form.control}
            name="code"
            render={({ field, fieldState }) => (
              <Field data-invalid={fieldState.invalid} className="space-y-1">
                <FieldLabel htmlFor={field.name} className="block text-sm">
                  Code
                </FieldLabel>

                <Input
                  {...field}
                  id={field.name}
                  type="text"
                  aria-invalid={fieldState.invalid}
                  aria-label={field.name}
                />

                {fieldState.invalid && (
                  <FieldError errors={[fieldState.error]} />
                )}
              </Field>
            )}
          />

          <Controller
            control={form.control}
            name="title"
            render={({ field, fieldState }) => (
              <Field data-invalid={fieldState.invalid} className="space-y-1">
                <FieldLabel htmlFor={field.name} className="block text-sm">
                  Title
                </FieldLabel>

                <Input
                  {...field}
                  id={field.name}
                  type="text"
                  aria-invalid={fieldState.invalid}
                  aria-label={field.name}
                />

                {fieldState.invalid && (
                  <FieldError errors={[fieldState.error]} />
                )}
              </Field>
            )}
          />

          <Controller
            control={form.control}
            name="description"
            render={({ field, fieldState }) => (
              <Field data-invalid={fieldState.invalid} className="space-y-1">
                <FieldLabel htmlFor={field.name} className="block text-sm">
                  Description
                </FieldLabel>

                <Textarea
                  {...field}
                  id={field.name}
                  aria-invalid={fieldState.invalid}
                  aria-label={field.name}
                  className="h-32 resize-none break-all"
                />

                {fieldState.invalid && (
                  <FieldError errors={[fieldState.error]} />
                )}
              </Field>
            )}
          />

          <DialogFooter>
            <DialogClose asChild>
              <Button type="button" variant="outline" disabled={isPending}>
                Cancel
              </Button>
            </DialogClose>

            <Button type="submit" disabled={isPending}>
              {isPending ? <Spinner /> : "Create"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
