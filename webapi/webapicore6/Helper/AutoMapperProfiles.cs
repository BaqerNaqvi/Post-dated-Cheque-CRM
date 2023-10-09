using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using webapicore6.Models;
using webapicore6.Models.Identity;

namespace webapicore6.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public string BuildCompanyBranchOfc(Agreement agreement)
        {
            var companyBranchOfc = agreement != null? agreement.Company.Name:"";

            if (!string.IsNullOrWhiteSpace(agreement.Floor))
            {
                companyBranchOfc += " - F " + agreement.Floor;
            }

            if (!string.IsNullOrWhiteSpace(agreement.OfficeNumber))
            {
                companyBranchOfc += " - O " + agreement.OfficeNumber;
            }

            if (!string.IsNullOrWhiteSpace(agreement.Branch))
            {
                companyBranchOfc += " - B " + agreement.Branch;
            }

            if (!string.IsNullOrWhiteSpace(agreement.WorkStation))
            {
                companyBranchOfc += " - WS " + agreement.WorkStation;
            }

            return companyBranchOfc;
        }
        public AutoMapperProfiles()
        {
            CreateMap<IdentityUser, User>();
            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Payment, PaymentListDto>()
            .ForMember(e => e.AgreementDates, PL => PL.MapFrom(q => q.Agreement.StartDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) + " : " + q.Agreement.EndDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)))
            .ForMember(e => e.CompanyName, PL => PL.MapFrom(q => q.Agreement != null? q.Agreement.Company.Name:""))
            .ForMember( e => e.CompanyBranchOfc, PL => PL.MapFrom(q => BuildCompanyBranchOfc(q.Agreement)))
            .ForMember(e => e.SenderBankName, PL => PL.MapFrom(src => src.SenderBank != null ? src.SenderBank.Name : null))
            .ForMember(e => e.ReceiverBankName, PL => PL.MapFrom(src => src.ReceiverBank != null ? src.ReceiverBank.Name : null));

            CreateMap<Agreement, AgreementDto>();
            CreateMap<Agreement, AgreementListDto>();
            CreateMap<AgreementDto, Agreement>();

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<Bank, BankDto>();
            CreateMap<BankDto, Bank>();
        }
    }
}
