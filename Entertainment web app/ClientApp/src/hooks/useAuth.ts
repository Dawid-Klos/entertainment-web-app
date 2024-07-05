import { useState } from "react";
import axios from "axios";

import { useNavigate, useLocation } from "react-router-dom";

import { LoginBody, RegisterBody } from "@commonTypes/auth.types";

export const useAuth = () => {
  const [submission, setSubmission] = useState({
    status: "",
    message: "",
    error: "",
  });

  const navigate = useNavigate();
  const location = useLocation();
  const currentPath = location.pathname;

  const handleSubmit = async (
    e: React.ChangeEvent<HTMLInputElement>,
    body: LoginBody | RegisterBody,
    endpoint: string,
  ) => {
    e.preventDefault();
    setSubmission({ status: "submitting", message: "", error: "" });

    try {
      const login = await axios.post(endpoint, body, {
        withCredentials: true,
      });

      const res = login.data;

      if (res.statusCode === 200 && res.status === "success") {
        setTimeout(() => {
          setSubmission({
            ...submission,
            status: "success",
            message: login.data.Message,
          });

          navigate("/");
        }, 500);
      } else {
        setSubmission({
          ...submission,
          status: "error",
          message:
            "There is some problem. Please, refresh the page and try again.",
        });
      }
    } catch (error: any) {
      const res = error.response.data;
      setSubmission({
        status: "error",
        message: "Following errors occured: ",
        error: res.length > 1 ? res[0] : res.Message,
      });
    }
  };

  const logout = async () => {
    try {
      setSubmission({
        status: "logging out",
        message: "Logging out...",
        error: "",
      });
      await axios.post("/api/auth/logout");

      setTimeout(() => {
        setSubmission({
          status: "success",
          message: "You have been logged out.",
          error: "",
        });
        navigate("/login");
      }, 500);
    } catch (error: any) {
      setSubmission({
        status: "error",
        message:
          "There is some problem. Please, refresh the page and try again.",
        error: error.response.data.Message,
      });

      // try to refresh the page to resolve the issue
      navigate(currentPath);
    }
  };

  return { submission, handleSubmit, logout };
};
