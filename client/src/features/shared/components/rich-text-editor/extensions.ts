import { Extension } from "@tiptap/core";
import StarterKit from "@tiptap/starter-kit";
import Image from "@tiptap/extension-image";

export const baseExtensions = [
  StarterKit.configure({
    trailingNode: false,
  }),
  Image,
];

type CreateComposerShortcutExtensionOptions = {
  onSubmit: () => void;
  isSubmitting: boolean;
};

export function createComposerShortcutExtension({
  onSubmit,
  isSubmitting,
}: CreateComposerShortcutExtensionOptions) {
  return Extension.create({
    name: "composerShortcut",
    priority: 1000,

    addKeyboardShortcuts() {
      return {
        Enter: () => {
          if (isSubmitting || this.editor.isEmpty) {
            return true;
          }

          onSubmit();
          return true;
        },
        "Shift-Enter": () => {
          if (
            this.editor.isActive("bulletList") ||
            this.editor.isActive("orderedList")
          ) {
            return this.editor.chain().focus().splitListItem("listItem").run();
          }

          return this.editor.chain().focus().setHardBreak().run();
        },
      };
    },
  });
}
