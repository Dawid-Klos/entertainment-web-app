export type LoginBody = {
  Email: string;
  Password: string;
};

export type RegisterBody = {
  Email: string;
  Firstname: string | null;
  Lastname: string | null;
  Password: string;
  ConfirmPassword: string | null;
};

export type User = {
  id: string;
  firstname: string;
  lastname: string;
  email: string;
};
