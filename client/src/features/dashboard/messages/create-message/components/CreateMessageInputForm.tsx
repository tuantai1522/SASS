import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import {
  AttachMessageButton,
  CreateMessageButton,
  RichTextEditorFooter,
  RichTextEditorFooterGroup,
  RichTextEditorInput,
  RichTextEditorRoot,
  RichTextEditorToolbar,
} from "@/features/shared";

import {
  type CreateMessageFormValues,
  createMessageSchema,
} from "../validators";
import { useMutation } from "@tanstack/react-query";
import { createMessageOptions } from "../create-message-options";
import { normalizeApiError } from "@/lib";
import { toast } from "sonner";

type CreateMessageInputFormProps = {
  conversationId: string;
};

export function CreateMessageInputForm({
  conversationId,
}: CreateMessageInputFormProps) {
  const form = useForm<CreateMessageFormValues>({
    resolver: zodResolver(createMessageSchema),
    defaultValues: {
      content: "",
    },
  });

  const { mutate, isPending } = useMutation({
    ...createMessageOptions(),
    onSuccess: async () => {
      form.reset({
        content: "",
      });
    },
    onError: (error) => {
      const normalizedError = normalizeApiError(error);
      toast.error(normalizedError.detail, { position: "bottom-right" });
    },
  });

  function handleSubmit(values: CreateMessageFormValues) {
    mutate({
      content: values.content,
      conversationId,
    });
  }

  return (
    <form>
      <Controller
        control={form.control}
        name="content"
        render={({ field }) => (
          <RichTextEditorRoot
            content={field.value}
            onChange={field.onChange}
            onSubmit={() => handleSubmit(form.getValues())}
            isSubmitting={isPending}
            placeholder={"Type a message"}
            className="m-2"
          >
            <RichTextEditorToolbar />
            <RichTextEditorInput />
            <RichTextEditorFooter>
              <RichTextEditorFooterGroup>
                <AttachMessageButton />
              </RichTextEditorFooterGroup>
              <RichTextEditorFooterGroup>
                <CreateMessageButton />
              </RichTextEditorFooterGroup>
            </RichTextEditorFooter>
          </RichTextEditorRoot>
        )}
      />
    </form>
  );
}
