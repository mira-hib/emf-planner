import { useEffect, useRef } from "react";
import { X } from "lucide-react";
export default function Modal({
  title,
  children,
  onClose,
  wide = false,
}: {
  title: string;
  children: React.ReactNode;
  onClose: () => void;
  wide?: boolean;
}) {
  const ref = useRef<HTMLElement>(null);
  useEffect(() => {
    const previous = document.activeElement as HTMLElement | null;
    ref.current
      ?.querySelector(".tool-dialog-body")
      ?.querySelector<HTMLElement>("input,select,textarea,button")
      ?.focus();
    return () => previous?.focus();
  }, []);
  return (
    <div className="modal-backdrop">
      <section
        ref={ref}
        className={`tool-dialog ${wide ? "wide" : ""}`}
        role="dialog"
        aria-modal="true"
        aria-label={title}
        onKeyDown={(e) => {
          if (e.key === "Escape") {
            e.stopPropagation();
            onClose();
          }
          if (e.key === "Tab") {
            const items = ref.current?.querySelectorAll<HTMLElement>(
              'button:not(:disabled),input:not(:disabled),select,textarea,[tabindex="0"]',
            );
            const first = items?.[0],
              last = items?.[items.length - 1];
            if (e.shiftKey && document.activeElement === first) {
              e.preventDefault();
              last?.focus();
            } else if (!e.shiftKey && document.activeElement === last) {
              e.preventDefault();
              first?.focus();
            }
          }
        }}
      >
        <header>
          <span>{title}</span>
          <button aria-label="Fermer la fenêtre" onClick={onClose}>
            <X size={14} />
          </button>
        </header>
        <div className="tool-dialog-body">{children}</div>
      </section>
    </div>
  );
}
