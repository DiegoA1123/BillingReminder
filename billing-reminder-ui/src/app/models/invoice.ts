export interface Invoice {
  id: string;
  clientId: string;
  clientEmail: string;
  status: string;
  total: number;
  createdAt: string;
}
