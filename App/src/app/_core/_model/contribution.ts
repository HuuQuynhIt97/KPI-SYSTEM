export interface Contribution {
  id: number;
  content: string;
  scoreType: string;
  createdBy: number;
  accountId: number;
  periodTypeId: number;
  period: number;
  modifiedBy: number | null;
  createdTime: string;
  modifiedTime: string | null;
}


export class Custom
{
  value: number;
  actionId: number;
  month: string;
}
