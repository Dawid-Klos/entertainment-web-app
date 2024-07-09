import axios from "axios";

export const AuthenticateUser = async (): Promise<boolean> => {
  const headers = {
    "Content-Type": "application/json",
  };

  try {
    const response = await axios.post("/api/auth", { headers });

    return (
      response.data.statusCode === 200 && response.data.status === "success"
    );
  } catch (error) {
    return false;
  }
};
