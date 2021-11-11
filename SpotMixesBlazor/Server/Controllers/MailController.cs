﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.ModelsData;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly MailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = mailService;
        }
        
        [HttpPost("SendEmailWithVerificationCode")]
        public async Task<ActionResult> SendEmailWithVerificationCode([FromBody] VerifyEmail verifyEmail)
        {
            try
            {
                await _mailService.SendEmailWithVerificationCode(verifyEmail);
                return Ok($"Se envió el código de verificación al correo {verifyEmail.ToEmail}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("No se ha podido enviar el código de verificación, por favor inténtelo más tarde.");
            }
        }
    }
}