// user.model.ts
export interface User {
  id: string | null;
  name?: string;  // Made optional
  email: string;
  role: string;
  token?: string;
}