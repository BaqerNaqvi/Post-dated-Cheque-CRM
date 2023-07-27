export class Bank {
  public id: number;
  public name: string;
  public branch?: string | null;
  public phone?: string | null;
  public fax?: string | null;
  public poBox?: string | null;
  public website?: string | null;
  public email?: string | null;

  constructor(
    id: number,
    name: string,
    branch?: string | null,
    phone?: string | null,
    fax?: string | null,
    poBox?: string | null,
    website?: string | null,
    email?: string | null
  ) {
    this.id = id;
    this.name = name;
    this.branch = branch;
    this.phone = phone;
    this.fax = fax;
    this.poBox = poBox;
    this.website = website;
    this.email = email;
  }
}