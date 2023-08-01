export class Payment {
    public id: number;
    public agreementId: number;
    public paymentMethod: number;
    public paymentDueDate: Date;
    public chequeNo?: string | null;
    public chequeDueDate?: Date | null;
    public senderBankId?: number | null;
    public receiverBankId?: number | null;
    public paymentStatus: number;
    public amount: number;
    public description?: string | null;
    public paymentClearanceDate?: Date | null;
    public agreementDates?: string | null;
    public companyName?: string | null;
    public companyBranchOfc?: string | null;
    public receiverBankName?: string | null;
    public senderBankName?: string | null;

    constructor(
        id: number,
        agreementId: number,
        paymentMethod: number,
        paymentDueDate: Date,
        paymentStatus: number,
        amount: number
    ) {
        this.id = id;
        this.agreementId = agreementId;
        this.paymentMethod = paymentMethod;
        this.paymentDueDate = paymentDueDate;
        this.paymentStatus = paymentStatus;
        this.amount = amount;
    }
}