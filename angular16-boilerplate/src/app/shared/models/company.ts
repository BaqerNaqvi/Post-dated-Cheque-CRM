export class Company {
    public id: number;
    public name: string;
    public address?: string | null;
    public website?: string | null;
    public email?: string | null;
    public phone?: string | null;
  
    constructor(
      id: number,
      name: string,
      address?: string | null,
      website?: string | null,
      email?: string | null,
      phone?: string | null
    ) {
      this.id = id;
      this.name = name;
      this.address = address;
      this.website = website;
      this.email = email;
      this.phone = phone;
    }
  }