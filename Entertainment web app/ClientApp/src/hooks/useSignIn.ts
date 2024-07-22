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
        setTimeout(() => {
          setSubmission({
            status: "success",
            message: "You have been logged in.",
          });
        }, 1000);

        setTimeout(() => {
          navigate("/");
        }, 2000);
      }

      if (res.statusCode === 400 && res.status === "error") {
        setSubmission({
          status: "error",
          message: "The password does not match or you do not have an account.",
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
