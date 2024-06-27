import "./Spinner.scss";

type SpinnerProps = {
  loading: boolean;
  variant: "primary" | "secondary";
};

const Spinner = ({ loading, variant }: SpinnerProps) => {
  return (
    <span
      className={`spinner ${loading && "spinner--active"} spinner--${variant}`}
    ></span>
  );
};

export default Spinner;
