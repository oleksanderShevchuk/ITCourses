﻿using System.Text;
using ITCoursesWeb.Data;
using ITCoursesWeb.DTOs;
using ITCoursesWeb.Interfaces;
using ITCoursesWeb.Models;
using Microsoft.EntityFrameworkCore;
using ITCoursesWeb.Repositories;
using ITCoursesWeb.DataAccess;

namespace ITCoursesWeb.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly SqPromoCode _sqPromoCode;
        private readonly AppDbContext _context;
        private readonly PromoCodeRepository _promoCodeRepository;

        public PromoCodeService(SqPromoCode sqPromoCode, AppDbContext context, PromoCodeRepository promoCodeRepository)
        {
            _sqPromoCode = sqPromoCode;
            _context = context;
            _promoCodeRepository = promoCodeRepository;
        }

        public IEnumerable<PromoCodeDto> GetAllByCourseId(string courseId)
        {
            var promoCodes = _sqPromoCode.GetPromoCodesByCourseId(courseId);
            foreach (var promoCode in promoCodes)
            {
                var person = _context.Persons.FirstOrDefault(p => p.Id == promoCode.PersonId);
                yield return new PromoCodeDto
                {
                    Id = promoCode.Id,
                    Code = promoCode.Code,
                    CourseId = promoCode.CourseId,
                    DateTo = promoCode.DateTo,
                    IsUsed = promoCode.IsUsed,
                    Percent = promoCode.Percent,
                    PersonEmail = person?.Email,
                };
            }
        }

        public async Task<PromoCodeDto> AddAsync(string courseId)
        {
            var promoCode = new PromoCode
            {
                Id = Guid.NewGuid().ToString(),
                Code = GeneratePromoCode(),
                CourseId = courseId,
                DateTo = DateTime.Now.AddDays(30),
                IsUsed = false,
                Percent = 5,
            };

            _sqPromoCode.AddPromoCode(promoCode);

            return new PromoCodeDto
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                CourseId = promoCode.CourseId,
                Percent = promoCode.Percent,
                DateTo = promoCode.DateTo,
                IsUsed = promoCode.IsUsed,
            };
        }

        public async Task<PromoCodeDto> EditAsync(string id, UpdatePromoCodeDto updatePromoCodeDto)
        {
            var promoCode = _sqPromoCode.GetPromoCodeById(id);
            if (promoCode == null)
                return null!;

            promoCode.IsUsed = Convert.ToBoolean(updatePromoCodeDto.IsUsed);
            promoCode.Percent = updatePromoCodeDto.Percent > 20 ? 20 : updatePromoCodeDto.Percent;
            promoCode.DateTo = updatePromoCodeDto.DateTo;

            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Email == updatePromoCodeDto.PersonEmail);
            if (person != null)
            {
                promoCode.Person = person;
                promoCode.PersonId = person.Id;
                person.PromoCodes.Add(promoCode);
            }

            _sqPromoCode.UpdatePromoCode(promoCode);

            return new PromoCodeDto
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                CourseId = promoCode.CourseId!,
                Percent = promoCode.Percent,
                DateTo = promoCode.DateTo,
                IsUsed = promoCode.IsUsed,
                PersonEmail = person?.Email!,
            };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var promoCode = _sqPromoCode.GetPromoCodeById(id);
            if (promoCode == null)
                return false;

            _sqPromoCode.DeletePromoCode(id);

            return true;
        }

        public void GeneratePromoCodes(int countPromoCodes, string courseId, DateTime dateTo, int discount)
        {
            try
            {
                _promoCodeRepository.GeneratePromoCodes(countPromoCodes, courseId, dateTo, discount);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while generating promo codes.", ex);
            }
        }

        private string GeneratePromoCode(int length = 15)
        {
            string code;
            bool codeExists;

            do
            {
                var random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder promoCode = new StringBuilder();

                for (int i = 0; i < length; i++)
                {
                    promoCode.Append(chars[random.Next(chars.Length)]);
                }

                code = promoCode.ToString();

                codeExists = _sqPromoCode.PromoCodeExists(code);
            }
            while (codeExists);

            return code;
        }
    }
}
