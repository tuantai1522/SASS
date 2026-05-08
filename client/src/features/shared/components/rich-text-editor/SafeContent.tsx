import { convertJsonToHtml } from "@/lib";
import DOMpurify from "dompurify";
import type { JSONContent } from "@tiptap/react";
import parse from "html-react-parser";

interface SafeContentProps {
  className?: string;
  content: JSONContent;
}
export function SafeContent({ className, content }: SafeContentProps) {
  const html = convertJsonToHtml(content);

  const clean = DOMpurify.sanitize(html);

  return <div className={className}>{parse(clean)}</div>;
}
