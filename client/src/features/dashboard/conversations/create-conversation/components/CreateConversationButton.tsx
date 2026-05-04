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
} from "@/features/shared";
import { createConversationOptions } from "../create-conversation-options.ts";
import { useMutation } from "@tanstack/react-query";
import { normalizeApiError } from "@/lib/normalize-api-error.ts";
import { toast } from "sonner";
import {
  type CreateConversationFormValues,
  createConversationSchema,
} from "../validators.ts";
import { Plus } from "lucide-react";
import { useState } from "react";

export function CreateConversationButton() {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);

  const form = useForm<CreateConversationFormValues>({
    resolver: zodResolver(createConversationSchema),
    defaultValues: {
      name: "",
    },
  });

  const { mutate, isPending } = useMutation({
    ...createConversationOptions(),
    onSuccess: async (response) => {
      setOpen(false);
      form.reset();

      await navigate({
        to: "/conversations/$conversationId",
        params: { conversationId: response.id },
      });
    },
    onError: (error) => {
      const normalizedError = normalizeApiError(error);
      toast.error(normalizedError.detail, { position: "bottom-right" });
    },
  });

  function onSubmit(values: CreateConversationFormValues) {
    console.log("Click");
    mutate(values);
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button
          type="button"
          variant="outline"
          className="w-full cursor-pointer hover:bg-accent hover:text-accent-foreground"
        >
          <Plus className="size-4" />
          Create conversation
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
            name="name"
            render={({ field, fieldState }) => (
              <Field data-invalid={fieldState.invalid} className="space-y-1">
                <FieldLabel htmlFor={field.name} className="block text-sm">
                  Name
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
