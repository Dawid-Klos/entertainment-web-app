import { useState } from "react";
import axios from "axios";

import { useNavigate } from "react-router-dom";

import { LoginBody } from "@config/formSchemas";

export const useSignIn = () => {
  const [submission, setSubmission] = useState({
    status: "",
    message: "",
  });

  const navigate = useNavigate();

  const signIn = async (body: LoginBody) => {
    setSubmission({ status: "submitting", message: "" });

    try {
      const login = await axios.post("/api/auth/login", body);

      const res = login.data;

      if (res.statusCode === 200 && res.status === "success") {
        setSubmission({
          status: "success",
          message: "You have been logged in.",
        });

        setTimeout(() => {
          navigate("/");
        }, 2000);
      }

      if (res.statusCode === 400 && res.status === "error") {
        setSubmission({
          status: "error",
          message: "Invalid email or password. Please, try again.",
        });
      }

      if (res.statusCode === 500 && res.status === "error") {
        setSubmission({
          status: "error",
          message:
            "There is some problem. Please, refresh the page or try again later.",
        });
      }
    } catch (error: any) {
      setSubmission({
        status: "error",
        message:
          "There is some problem. Please, refresh the page and try again.",
      });
    }
  };

  const signOut = async () => {
    setSubmission({ status: "signing out", message: "" });

    try {
      await axios.post("/api/auth/logout");

      setSubmission({
        status: "success",
        message: "You have been logged out.",
      });

      navigate("/login");
    } catch (error: any) {
      setSubmission({
        status: "error",
        message:
          "There is some problem. Please, refresh the page and try again.",
      });
    }
  };

  return { submission, signIn, signOut };
};
