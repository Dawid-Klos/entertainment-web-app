type IconProps = {
  className?: string;
};

const MoreIcon = ({ className }: IconProps) => (
  <svg
    width="15"
    height="4"
    viewBox="0 0 15 4"
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
    className={className}
  >
    <circle cx="1.75" cy="1.75" r="1.75" fill="currentColor" />
    <circle cx="7.25" cy="1.75" r="1.75" fill="currentColor" />
    <circle cx="12.75" cy="1.75" r="1.75" fill="currentColor" />
  </svg>
);

export default MoreIcon;
