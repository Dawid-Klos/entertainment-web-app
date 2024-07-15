import { z } from "zod";

export const loginSchema = z.object({
  email: z.string().email({ message: "Please enter a valid email address" }),
  password: z
    .string()
    .min(8, { message: "Password must be at least 8 characters" }),
});

export type LoginBody = z.infer<typeof loginSchema>;

export const registerSchema = z
  .object({
    email: z.string().email({ message: "Please enter a valid email address" }),
    firstName: z
      .string()
      .min(4, { message: "First name must be at least 4 characters" }),
    lastName: z
      .string()
      .min(4, { message: "Last name must be at least 4 characters" }),
    password: z
      .string()
      .min(8, { message: "Password must be at least 8 characters" }),
    confirmPassword: z
      .string()
      .min(8, { message: "Password must be at least 8 characters" }),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });

export type RegisterBody = z.infer<typeof registerSchema>;
