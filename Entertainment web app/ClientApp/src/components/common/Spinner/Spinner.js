import "./Spinner.scss";

const Spinner = ({ loading, variant }) => {
  return (
    <span
      className={`spinner ${loading && "spinner--active"} spinner--${variant}`}
    ></span>
  );
};

export default Spinner;
