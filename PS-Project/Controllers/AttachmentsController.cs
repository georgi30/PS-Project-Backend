using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Persistence.Entities;
using PS_Project_Model.Resources;
using PS_Project_Model.Resources.Attachments;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project.Controllers
{
    [Authorize]
    [Route("/api/attachments")]
    [Produces("application/json")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentsService _attachmentsService;
        private readonly IMapper _mapper;
        private readonly IAttachmentUtils _utils;
        private readonly IRequestUtils _requestUtils;

        public AttachmentsController(IAttachmentsService attachmentsService, IMapper mapper, 
                                    IAttachmentUtils utils, IRequestUtils requestUtils)
        {
            _attachmentsService = attachmentsService;
            _mapper = mapper;
            _utils = utils;
            _requestUtils = requestUtils;
        }

        /// <summary>
        /// Lists all attachments.
        /// </summary>
        /// <returns>List of attachments.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AttachmentsResource>), 200)]
        public async Task<IEnumerable<AttachmentsResource>> ListAsync()
        {
            var attachments = await _attachmentsService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Attachment>, IEnumerable<AttachmentsResource>>(attachments);

            return resources;
        }

        /// <summary>
        /// Gets an existing attachment according to an identifier.
        /// </summary>
        /// <param name="id">Attachment identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AttachmentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _attachmentsService.GetAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var screenResource = _mapper.Map<Attachment, AttachmentsResource>(result.Resource);
            return Ok(screenResource);
        }
        
        /// <summary>
        /// Gets an existing attachment according to an slug and returns file in response.
        /// </summary>
        /// <param name="fileSlug">Attachment slug.</param>
        /// <returns>Response for the request.</returns>
        [AllowAnonymous]
        [HttpGet("/{fileSlug}")]
        [ProducesResponseType(typeof(AttachmentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetFileAsync(string fileSlug)
        {
            var result = await _attachmentsService.GetByFileSlugAsync(fileSlug);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }
            
            
            var file = result.Resource;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), file.FilePath);
            var attachment = System.IO.File.OpenRead(filepath);
            
            return new FileStreamResult(attachment, MediaTypeHeaderValue.Parse("image/jpg"));
        }
        
        /// <summary>
        /// Gets an existing attachment according to an slug.
        /// </summary>
        /// <param name="fileSlug">Attachment slug.</param>
        /// <returns>Response for the request.</returns>
        [AllowAnonymous]
        [HttpGet("getAttachmentByFileSlug/{fileSlug}")]
        [ProducesResponseType(typeof(AttachmentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetByFileSlugAsync(string fileSlug)
        {
            var result = await _attachmentsService.GetByFileSlugAsync(fileSlug);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
        
        /// <summary>
        /// Saves a new attachment.
        /// </summary>
        /// <param name="resource">Attachment data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AttachmentsResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromForm] SaveAttachmentsResource resource)
        {
            
            var attachment = _utils.ReadyForCreation(resource);
            attachment.WhoUserId = _requestUtils.GetUserIdFromToken(await HttpContext.GetTokenAsync("access_token"));
            
            var result = await _attachmentsService.SaveAsync(attachment);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var attachmentsResource = _mapper.Map<Attachment, AttachmentsResource>(result.Resource);
            return Ok(attachmentsResource);
        }

        /// <summary>
        /// Updates an existing attachment according to an identifier.
        /// </summary>
        /// <param name="id">Attachment identifier.</param>
        /// <param name="resource">Updated Attachment data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AttachmentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAttachmentsResource resource)
        {
            var attachment = _mapper.Map<SaveAttachmentsResource, Attachment>(resource);
            var result = await _attachmentsService.UpdateAsync(id, attachment);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var attachmentsResource = _mapper.Map<Attachment, AttachmentsResource>(result.Resource);
            return Ok(attachmentsResource);
        }

        /// <summary>
        ///    Deletes a given attachment according to an identifier.
        /// </summary>
        /// <param name="id">Attachment identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AttachmentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _attachmentsService.GetAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }


            var attachmentsResource = _utils.DeleteByUpdate(result.Resource);
            return Ok(attachmentsResource);
        }
    }
}