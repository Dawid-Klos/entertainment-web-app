type BookmarkProps = {
  variant: "filled" | "outlined";
  className?: string;
};

const Bookmark = ({ variant, className }: BookmarkProps) => {
  return (
    <svg
      className={className}
      width="12"
      height="14"
      xmlns="http://www.w3.org/2000/svg"
    >
      {variant === "outlined" ? (
        <path
          d="m10.518.75.399 12.214-5.084-4.24-4.535 4.426L.75 1.036l9.768-.285Z"
          stroke="currentColor"
          strokeWidth="1.5"
          fill="none"
        />
      ) : (
        <path
          d="M10.61 0c.14 0 .273.028.4.083a1.03 1.03 0 0 1 .657.953v11.928a1.03 1.03 0 0 1-.656.953c-.116.05-.25.074-.402.074-.291 0-.543-.099-.756-.296L5.833 9.77l-4.02 3.924c-.218.203-.47.305-.756.305a.995.995 0 0 1-.4-.083A1.03 1.03 0 0 1 0 12.964V1.036A1.03 1.03 0 0 1 .656.083.995.995 0 0 1 1.057 0h9.552Z"
          fill="currentColor"
        />
      )}
    </svg>
  );
};

export default Bookmark;
