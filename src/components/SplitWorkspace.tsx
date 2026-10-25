import {
  useEffect,
  useRef,
  useState,
  type ReactNode,
  type CSSProperties,
} from "react";
export default function SplitWorkspace({
  children,
  vertical = false,
  storageKey,
  defaultWidth = 350,
}: {
  children: ReactNode[];
  vertical?: boolean;
  storageKey?: string;
  defaultWidth?: number;
}) {
  const key = `emf-pane-width-${storageKey ?? (vertical ? "vertical" : "map")}`,
    host = useRef<HTMLDivElement>(null),
    [width, setWidth] = useState(() => {
      try {
        return Math.max(
          260,
          Math.min(900, Number(localStorage.getItem(key)) || defaultWidth),
        );
      } catch {
        return defaultWidth;
      }
    });
  useEffect(() => {
    const element = host.current;
    if (!element) return;
    const constrain = () =>
      setWidth((current) =>
        Math.min(current, Math.max(260, element.clientWidth - 240)),
      );
    const observer = new ResizeObserver(constrain);
    observer.observe(element);
    // A rapid grow/shrink can return to the last observed size before the next
    // ResizeObserver delivery, while End has already expanded the stored width.
    window.addEventListener("resize", constrain);
    return () => {
      observer.disconnect();
      window.removeEventListener("resize", constrain);
    };
  }, []);
  const change = (value: number) => {
    const next = Math.max(
      260,
      Math.min(value, (host.current?.clientWidth ?? 1000) - 240),
    );
    setWidth(next);
    try {
      localStorage.setItem(key, String(Math.round(next)));
    } catch {
      /* Pane size is optional. */
    }
  };
  return (
    <div
      ref={host}
      className={`split-workspace${vertical ? " vertical-workspace" : ""}`}
      style={{ "--property-width": `${width}px` } as CSSProperties}
    >
      {children[0]}
      <div
        className="pane-divider"
        role="separator"
        aria-label="Largeur du panneau de propriétés"
        aria-orientation="vertical"
        aria-valuenow={Math.round(width)}
        tabIndex={0}
        onPointerDown={(e) => {
          e.currentTarget.setPointerCapture(e.pointerId);
          e.preventDefault();
        }}
        onPointerMove={(e) => {
          if (e.currentTarget.hasPointerCapture(e.pointerId))
            change(
              e.clientX - (host.current?.getBoundingClientRect().left ?? 0) - 5,
            );
        }}
        onPointerUp={(e) => {
          if (e.currentTarget.hasPointerCapture(e.pointerId))
            e.currentTarget.releasePointerCapture(e.pointerId);
        }}
        onKeyDown={(e) => {
          if (["ArrowLeft", "ArrowRight", "Home", "End"].includes(e.key)) {
            e.preventDefault();
            change(
              e.key === "Home"
                ? 350
                : e.key === "End"
                  ? (host.current?.clientWidth ?? 1000) - 240
                  : width + (e.key === "ArrowLeft" ? -10 : 10),
            );
          }
        }}
      />
      {children[1]}
    </div>
  );
}
