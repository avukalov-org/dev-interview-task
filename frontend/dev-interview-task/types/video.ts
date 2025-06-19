export type Video = {
  id: string;
  title: string;
  isPremium: boolean;
  price: number;
  currency: string;
  status: string;
  userId: string;
  isPurchased: boolean;
  uploadId?: string | null;
  assetId?: string | null;
  playbackId?: string | null;
};
