import "./globals.css";
import { Button } from "@/components/ui/button";
import { ArrowUpIcon } from "lucide-react";
import { ThemeProvider } from "@/components/ui/theme-provider";
import { ThemeToggle } from "@/components/ui/theme-toggle";

function App() {
  return (
    <div className="flex flex-wrap items-center gap-2 md:flex-row">
      <Button variant="outline">Button</Button>
      <Button variant="outline" size="icon" aria-label="Submit">
        <ArrowUpIcon />
      </Button>

      <ThemeProvider defaultTheme="dark" storageKey="sass-theme">
        <ThemeToggle />
      </ThemeProvider>
    </div>
  );
}

export default App;
