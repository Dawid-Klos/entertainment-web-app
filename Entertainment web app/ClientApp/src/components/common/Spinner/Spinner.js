import "./Spinner.scss";

const Spinner = ({ loading }) => {
  return <span className={`spinner ${loading && "spinner--active"}`}></span>;
};

export default Spinner;
