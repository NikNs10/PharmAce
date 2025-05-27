// user.model.ts
export interface User {
  id: string | null;
  name?: string;  // Made optional
  email: string;
  role: string;
  token?: string;
}

export interface Users {
  userId?: string ;
  userName: string;
  email: string;
  phoneNumber: string;
  role: string; // e.g., Admin, Doctor, Supplier
}
