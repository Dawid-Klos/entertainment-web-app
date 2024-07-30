import axios from "axios";

export const AuthenticateUser = async () => {
  const headers = {
    "Content-Type": "application/json",
  };

  try {
    const res = await axios.post("/api/auth", { headers });

    if (res.data.statusCode !== 200 || res.data.status !== "success") {
      return false;
    }
    const { id, firstname, lastname, email } = res.data.data[0];

    return { id, firstname, lastname, email };
  } catch (error) {
    return false;
  }
};
