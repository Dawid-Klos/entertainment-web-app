import { useState } from "react";
import axios from "axios";

import { useNavigate } from "react-router-dom";

export const useAuth = () => {
  const [submission, setSubmission] = useState({ status: "", message: "" });
  const navigate = useNavigate();

  const handleSubmit = async (e, body, endpoint) => {
    e.preventDefault();
    setSubmission({ status: "submitting", message: "", errors: {} });

    try {
      const login = await axios.post(endpoint, body, {
        withCredentials: true,
      });

      if (login.status === 200 && login.data.isSuccess) {
        setTimeout(() => {
          setSubmission({
            ...submission,
            status: "success",
            message: login.data.Message,
          });

          navigate("/Library");
        }, 500);
      } else {
        setSubmission({
          ...submission,
          status: "error",
          message:
            "There is some problem. Please, refresh the page and try again.",
        });
      }
    } catch (error) {
      if (error.response.data.Message) {
        setSubmission({
          status: "error",
          message: error.response.data.Message,
          errors: error.response.data.Message,
        });
      } else {
        const errors = Object.values(error.response.data.errors).flat(1);

        setSubmission({
          status: "error",
          message: "Following errors occured: ",
          errors: errors,
        });
      }
    }
  };

  return { submission, handleSubmit };
};
