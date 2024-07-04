import { isRouteErrorResponse, useRouteError } from "react-router-dom";

import "./ErrorBoundary.scss";

type ErrorMessage = {
  [key: number]: string;
  default: string;
};

const errorMessage: ErrorMessage = {
  400: "Something went wrong with your request. Please refresh the page and try again.",
  401: "You are not authorized to view this page.",
  404: "The page you are looking for does not exist.",
  500: "An unexpected error occured. We're sorry about it, please try again later.",
  default:
    "An unexpected error occured. We're sorry about it, please try again later.",
};

const ErrorBoundary = () => {
  const error: any = useRouteError();

  return (
    <>
      <div className="error">
        <h1 className="error__title">Ooops, something went wrong...</h1>
        <p className="error__message">
          {isRouteErrorResponse(error)
            ? errorMessage[error.status]
            : errorMessage.default}
        </p>
      </div>
    </>
  );
};

export default ErrorBoundary;
