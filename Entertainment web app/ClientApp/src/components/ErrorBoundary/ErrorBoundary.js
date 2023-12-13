import Layout from "../Layout";
import "./ErrorBoundary.scss";
import { useRouteError } from "react-router-dom";

const errorMessage = {
  404: "This page doesn't exist!",
  401: "You aren't authorized to see this",
  500: "Looks like our API is down",
  503: "Looks like our API is down",
  418: "🫖",
  default:
    "An unexpected error occured. We're sorry about it, please try again later.",
};

const ErrorBoundary = () => {
  const errorData = useRouteError();
  console.log(errorData);
  return (
    <>
      <Layout />
      <div className="error">
        <h1 className="error__title">Ooops, something went wrong...</h1>
        <p className="error__message">{errorData?.message}</p>
      </div>
    </>
  );
};

export default ErrorBoundary;
