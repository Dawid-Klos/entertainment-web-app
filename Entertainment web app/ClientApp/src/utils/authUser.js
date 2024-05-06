import axios from "axios";
export const AuthenticateUser = async () => {
  const headers = {
    "Content-Type": "application/json",
  };

  try {
    const response = await axios.post("/api/auth", { headers });

    console.log(response.data);

    if (
      response.data.statusCode === 200 &&
      response.data.status === "success"
    ) {
      return true;
    } else {
      return false;
    }
  } catch (error) {
    return null;
  }
};
