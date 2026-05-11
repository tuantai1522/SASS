import { createContext, type ReactNode, useContext, useEffect } from "react";
import { type Editor, useEditorState } from "@tiptap/react";
import { EditorContent, useEditor } from "@tiptap/react";
import {
  Bold,
  Code,
  ImageIcon,
  Italic,
  ListIcon,
  ListOrdered,
  Redo,
  Send,
  Strikethrough,
  Undo,
} from "lucide-react";
import { cn } from "@/lib";
import {
  baseExtensions,
  Button,
  createComposerShortcutExtension,
  Toggle,
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/features/shared";
import { Placeholder } from "@tiptap/extensions/placeholder";

type RichTextEditorContextValue = {
  editor: Editor;
  content: string;
  onSubmit: () => void;
  isSubmitting: boolean;
};

const RichTextEditorContext = createContext<
  RichTextEditorContextValue | undefined
>(undefined);

type RichTextEditorRootProps = {
  children: ReactNode;
  className?: string;
  content: string;
  onChange: (content: string) => void;
  onSubmit: () => void;
  isSubmitting: boolean;
  placeholder: string;
};

function RichTextEditorRoot({
  children,
  className,
  content,
  onChange,
  onSubmit,
  isSubmitting,
  placeholder,
}: RichTextEditorRootProps) {
  const editor = useEditor({
    immediatelyRender: false,
    extensions: [
      ...baseExtensions,
      createComposerShortcutExtension({
        onSubmit,
        isSubmitting,
      }),
      Placeholder.configure({
        placeholder,
      }),
    ],
    content: () => {
      try {
        return JSON.parse(content);
      } catch {
        return "";
      }
    },
    editorProps: {
      attributes: {
        class: cn(
          "max-w-none min-h-[125px] px-4 py-3 focus:outline-none",
          "[&_.ProseMirror]:min-h-[125px] [&_.ProseMirror]:outline-none",
          "[&_ol]:list-decimal [&_ol]:pl-5 [&_ul]:list-disc [&_ul]:pl-5",
          "[&_p]:min-h-[1.25rem]",
        ),
      },
    },
    onUpdate: ({ editor }) => {
      onChange?.(JSON.stringify(editor.getJSON()));
    },
  });

  // Listen to change of content ,if it's empty => reset content in tiptap
  useEffect(() => {
    if (content === "") {
      editor?.commands.clearContent();
    }
  }, [content, editor]);

  if (!editor) {
    return;
  }

  return (
    <RichTextEditorContext.Provider
      value={{ editor, content, onSubmit, isSubmitting }}
    >
      <div
        className={cn(
          "overflow-hidden rounded-lg border border-input bg-background dark:bg-input/30",
          className,
        )}
      >
        {children}
      </div>
    </RichTextEditorContext.Provider>
  );
}

function useRichTextContext() {
  const context = useContext(RichTextEditorContext);

  if (context === undefined) {
    throw new Error(
      "useRichTextContext must be used within RichTextEditorContext",
    );
  }

  return context;
}

function RichTextEditorToolbar() {
  const { editor } = useRichTextContext();

  const editorState = useEditorState({
    editor,
    selector: ({ editor }) => {
      return {
        isBold: editor.isActive("bold"),
        isItalic: editor.isActive("italic"),
        isStrike: editor.isActive("strike"),
        isBulletList: editor.isActive("bulletList"),
        isOrderedList: editor.isActive("orderedList"),
        canUndo: editor.can().undo(),
        canRedo: editor.can().redo(),
      };
    },
  });

  return (
    <div className="border border-input border-t-0 border-x-0 rounde-t-lg p-2 bg-card flex flex-wrap gap-1 items-center">
      <TooltipProvider>
        <div className="flex flex-wrap gap-1">
          <Tooltip>
            <TooltipTrigger asChild>
              <Toggle
                size="sm"
                pressed={editorState.isBold}
                onPressedChange={() =>
                  editor.chain().focus().toggleBold().run()
                }
                className={cn(
                  editorState.isBold && "bg-muted text-muted-foreground",
                )}
              >
                <Bold className="size-4" />
              </Toggle>
            </TooltipTrigger>
            <TooltipContent>Bold</TooltipContent>
          </Tooltip>

          <Tooltip>
            <TooltipTrigger asChild>
              <Toggle
                size="sm"
                pressed={editorState.isItalic}
                onPressedChange={() =>
                  editor.chain().focus().toggleItalic().run()
                }
                className={cn(
                  editorState.isItalic && "bg-muted text-muted-foreground",
                )}
              >
                <Italic className="size-4" />
              </Toggle>
            </TooltipTrigger>
            <TooltipContent>Italic</TooltipContent>
          </Tooltip>

          <Tooltip>
            <TooltipTrigger asChild>
              <Toggle
                size="sm"
                pressed={editorState.isStrike}
                onPressedChange={() =>
                  editor.chain().focus().toggleStrike().run()
                }
                className={cn(
                  editorState.isStrike && "bg-muted text-muted-foreground",
                )}
              >
                <Strikethrough className="size-4" />
              </Toggle>
            </TooltipTrigger>
            <TooltipContent>Strike</TooltipContent>
          </Tooltip>

          <Tooltip>
            <TooltipTrigger asChild>
              <Toggle
                size="sm"
                pressed={editor.isActive("codeBlock")}
                onPressedChange={() =>
                  editor.chain().focus().toggleCodeBlock().run()
                }
                className={cn(
                  editor.isActive("codeBlock") &&
                    "bg-muted text-muted-foreground",
                )}
              >
                <Code className="size-4" />
              </Toggle>
            </TooltipTrigger>
            <TooltipContent>Code block</TooltipContent>
          </Tooltip>
        </div>

        <div className="w-px h-6 bg-border mx-2"></div>

        <div className="flex flex-wrap gap-1">
          <Tooltip>
            <TooltipTrigger asChild>
              <Toggle
                size="sm"
                pressed={editorState.isBulletList}
                onPressedChange={() =>
                  editor.chain().focus().toggleBulletList().run()
                }
                className={cn(
                  editorState.isBulletList && "bg-muted text-muted-foreground",
                )}
              >
                <ListIcon className="size-4" />
              </Toggle>
            </TooltipTrigger>
            <TooltipContent>Bullet list</TooltipContent>
          </Tooltip>

          <Tooltip>
            <TooltipTrigger asChild>
              <Toggle
                size="sm"
                pressed={editorState.isOrderedList}
                onPressedChange={() =>
                  editor.chain().focus().toggleOrderedList().run()
                }
                className={cn(
                  editorState.isOrderedList && "bg-muted text-muted-foreground",
                )}
              >
                <ListOrdered className="size-4" />
              </Toggle>
            </TooltipTrigger>
            <TooltipContent>Ordered list</TooltipContent>
          </Tooltip>
        </div>

        <div className="w-px h-6 bg-border mx-2"></div>

        <div className="flex flex-wrap gap-1">
          <Tooltip>
            <TooltipTrigger asChild>
              <Button
                size="sm"
                variant="ghost"
                type="button"
                disabled={!editorState.canUndo}
                onClick={() => editor.chain().focus().undo().run()}
              >
                <Undo className="size-4" />
              </Button>
            </TooltipTrigger>
            <TooltipContent>Undo</TooltipContent>
          </Tooltip>

          <Tooltip>
            <TooltipTrigger asChild>
              <Button
                size="sm"
                variant="ghost"
                type="button"
                disabled={!editorState.canRedo}
                onClick={() => editor.chain().focus().redo().run()}
              >
                <Redo className="size-4" />
              </Button>
            </TooltipTrigger>
            <TooltipContent>Redo</TooltipContent>
          </Tooltip>
        </div>
      </TooltipProvider>
    </div>
  );
}

function RichTextEditorInput({ className }: { className?: string }) {
  const { editor } = useRichTextContext();

  return (
    <EditorContent
      className={cn("max-h-[240px] overflow-y-auto", className)}
      editor={editor}
    />
  );
}

function RichTextEditorFooter({
  children,
  className,
}: {
  children: ReactNode;
  className?: string;
}) {
  return (
    <div
      className={cn(
        "flex items-center justify-between gap-3 border-t border-input bg-background/80 px-3 py-3 dark:bg-input/10",
        className,
      )}
    >
      {children}
    </div>
  );
}

function RichTextEditorFooterGroup({
  children,
  className,
}: {
  children: ReactNode;
  className?: string;
}) {
  return (
    <div className={cn("flex items-center gap-2", className)}>{children}</div>
  );
}

function CreateMessageButton() {
  const { onSubmit, isSubmitting, editor } = useRichTextContext();

  return (
    <Button
      // This will be disabled when calling api or content is empty
      disabled={isSubmitting || editor.isEmpty}
      type="button"
      size="sm"
      variant="default"
      onClick={onSubmit}
    >
      <Send className="size-4 mr-1" />
      Create
    </Button>
  );
}

function AttachMessageButton() {
  return (
    <Button type="button" size="sm" variant="outline">
      <ImageIcon className="size-4 mr-1" />
      Attach
    </Button>
  );
}

export {
  CreateMessageButton,
  AttachMessageButton,
  RichTextEditorFooter,
  RichTextEditorFooterGroup,
  RichTextEditorInput,
  RichTextEditorRoot,
  RichTextEditorToolbar,
  useRichTextContext,
};
