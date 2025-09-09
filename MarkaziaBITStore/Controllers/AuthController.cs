//using MarkaziaBITStore.DTOs;
//using MarkaziaBITStore.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace MarkaziaBITStore.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserService _userService;

//        public AuthController(UserService userService)
//        {
//            _userService = userService;
//        }

//        [HttpPost("register")]
//        public async Task<ActionResult<UserResponseDto>> Register(RegisterDto dto)
//        {
//            var result = await _userService.Register(dto);
//            if (result == null) return BadRequest("Email already exists");
//            return Ok(result);
//        }

//        [HttpPost("login")]
//        public async Task<ActionResult<UserResponseDto>> Login(LoginDto dto)
//        {
//            var result = await _userService.Login(dto);
//            if (result == null) return Unauthorized("Invalid credentials");
//            return Ok(result);
//        }

//        [AllowAnonymous]
//        [HttpPost("request-password-reset")]
//        public async Task<IActionResult> RequestPasswordReset(RequestPasswordResetDto dto)
//        {
//            var token = await _userService.RequestPasswordResetAsync(dto.Email);
//            if (token == null) return NotFound(new { message = "Email not found" });

//            // In real app → send token via email
//            return Ok(new { message = "Password reset requested. Use the token to reset.", token });
//        }

//        [AllowAnonymous]
//        [HttpPost("reset-password")]
//        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
//        {
//            var success = await _userService.ResetPasswordAsync(dto.Token, dto.NewPassword);
//            if (!success) return BadRequest(new { message = "Invalid or expired token" });

//            return Ok(new { message = "Password reset successful" });
//        }

//        [Authorize]
//        [HttpGet("me")]
//        public ActionResult<object> GetCurrentUser()
//        {
//            var claims = User.Claims.ToDictionary(c => c.Type, c => c.Value);
//            return Ok(claims);
//        }
//    }
//}
