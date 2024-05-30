import Layout from "../../Layout";
import "./ErrorBoundary.scss";
import { useRouteError } from "react-router-dom";

const errorMessage = {
  401: "You are not authorized to view this page.",
  404: "The page you are looking for does not exist.",
  500: "An unexpected error occured. We're sorry about it, please try again later.",
  default:
    "An unexpected error occured. We're sorry about it, please try again later.",
};

const ErrorBoundary = () => {
  const errorData = useRouteError();

  return (
    <>
      <Layout />
      <div className="error">
        <h1 className="error__title">Ooops, something went wrong...</h1>
        <p className="error__message">{errorMessage[errorData.message]}</p>
      </div>
    </>
  );
};

export default ErrorBoundary;
