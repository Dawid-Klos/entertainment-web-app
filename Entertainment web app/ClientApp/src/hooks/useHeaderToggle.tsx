import { useState, useEffect, useCallback } from "react";

type HeaderState = "on" | "off" | "hiding";

const useHeaderToggle = () => {
  const [headerState, setHeaderState] = useState<HeaderState>("off");

  const toggleHeaderState = () => {
    setHeaderState((prevState) => (prevState === "on" ? "hiding" : "on"));
  };

  const handleClickOutside = useCallback((event: MouseEvent) => {
    const target = event.target as HTMLElement;

    if (!target.closest(".header")) {
      setHeaderState("hiding");
    }
  }, []);

  useEffect(() => {
    if (headerState === "hiding") {
      window.removeEventListener("click", handleClickOutside);

      setTimeout(() => {
        setHeaderState("off");
      }, 500);
    }

    if (headerState === "on") {
      window.addEventListener("click", handleClickOutside);
    }

    return () => {
      window.removeEventListener("click", handleClickOutside);
    };
  }, [headerState, handleClickOutside]);

  return { headerState, toggleHeaderState };
};

export default useHeaderToggle;
