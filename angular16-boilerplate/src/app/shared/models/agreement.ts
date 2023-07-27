export class Agreement {
    public id: number;
    public companyId: number;
    public startDate: Date;
    public endDate: Date;
    public description?: string | null;
    public floor?: string | null;
    public officeNumber?: string | null;
    public section?: string | null;
    public workStation?: string | null;
  
    constructor(
      id: number,
      companyId: number,
      startDate: Date,
      endDate: Date,
      description?: string | null,
      floor?: string | null,
      officeNumber?: string | null,
      section?: string | null,
      workStation?: string | null
    ) {
      this.id = id;
      this.companyId = companyId;
      this.startDate = startDate;
      this.endDate = endDate;
      this.description = description;
      this.floor = floor;
      this.officeNumber = officeNumber;
      this.section = section;
      this.workStation = workStation;
    }
  }