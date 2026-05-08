import { generateHTML, type JSONContent } from "@tiptap/react";
import { baseExtensions } from "@/features/shared";

export function convertJsonToHtml(jsonContent: JSONContent) {
  try {
    const content =
      typeof jsonContent === "string" ? JSON.parse(jsonContent) : jsonContent;

    return generateHTML(content, baseExtensions);
  } catch {
    console.error("Error converting json to html");

    return " ";
  }
}
