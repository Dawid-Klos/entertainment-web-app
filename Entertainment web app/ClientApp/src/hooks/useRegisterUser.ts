import { useState } from "react";
import axios from "axios";

import { useNavigate } from "react-router-dom";

import { RegisterBody } from "@config/formSchemas";

export const useRegisterUser = () => {
  const [submission, setSubmission] = useState({
    status: "",
    message: "",
  });

  const navigate = useNavigate();

  const createAccount = async (body: RegisterBody) => {
    setSubmission({ status: "submitting", message: "" });

    try {
      const login = await axios.post("/api/auth/register", body);

      const res = login.data;

      if (res.statusCode === 200 && res.status === "success") {
        setSubmission({
          status: "success",
          message: "Your account has been created successfully.",
        });

        setTimeout(() => {
          navigate("/");
        }, 1500);
      }

      if (res.statusCode === 400 && res.status === "error") {
        console.log(res);
        setSubmission({
          status: "error",
          message: res.error.description,
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

  return { submission, createAccount };
};
