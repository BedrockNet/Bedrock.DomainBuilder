using System;

namespace Bedrock.DomainBuilder.Sections.ControllerApi
{
    public class SectionApiMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionApiMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(2), "#region Api Methods");
            Add(false, Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region Api Methods");
            BuildMethods();
            Add(false, Tab(2), "#endregion");
        }
        #endregion

        #region Private Methods
        private void BuildMethods()
        {
            if (Settings.IsIncludeCrud)
            {
                BuildGet();
                BuildGetById();
                BuildPost();
                BuildPut();
                BuildDelete();
            }
        }

        private void BuildGet()
        {
            Add(Tab(2), "public async Task<IHttpActionResult> Get()");
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("var returnValue = await {0}Service.Get{0}All();", EntityName));
            Add(Tab(3), string.Format("EnsureResult(returnValue, HttpStatusCode.NotFound, \"No {0}s found\", \"No {0}s found\");", EntityName));
            Add(Tab(3), "return Ok(returnValue);");
            Add(Tab(2), "}", NewLine());
        }

        private void BuildGetById()
        {
            Add(Tab(2), "public async Task<IHttpActionResult> Get(int id)");
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("var returnValue = await {0}Service.Get{0}ById(id);", EntityName));
            Add(Tab(3), string.Format("EnsureResult(returnValue, HttpStatusCode.NotFound, string.Format(\"No {0} with Id = {{0}}\", id), \"{0} Id Not Found\");", EntityName));
            Add(Tab(3), "return Ok(returnValue);");
            Add(Tab(2), "}", NewLine());
        }

        private void BuildPost()
        {
            Add(Tab(2), string.Format("public async Task<IHttpActionResult> Post([FromBody]{0}Contract contract)", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), "EnsureModel(contract, \"The contract is malformed\");", NewLine());
            Add(Tab(3), string.Format("var response = await {0}Service.Add{0}(contract);", EntityName));
            Add(Tab(3), string.Format("EnsureValidity(HttpStatusCode.OK, response.ValidationState, \"{0} invalid for insert\");", EntityName), NewLine());
            Add(Tab(3), "await Task.WhenAll(CurrentSession.CompleteAsync(response));", NewLine());
            Add(Tab(3), "return Created(GetCreatedLocation(response.Entity), response.Contract);");
            Add(Tab(2), "}", NewLine());
        }

        private void BuildPut()
        {
            Add(Tab(2), string.Format("public async Task<IHttpActionResult> Put([FromBody]{0}Contract contract)", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), "EnsureModel(contract, \"The contract is malformed\");", NewLine());
            Add(Tab(3), string.Format("var response = await {0}Service.Update{0}(contract);", EntityName));
            Add(Tab(3), string.Format("EnsureValidity(HttpStatusCode.OK, response.ValidationState, \"{0} invalid for update\");", EntityName), NewLine());
            Add(Tab(3), "await Task.WhenAll(CurrentSession.CompleteAsync(response));", NewLine());
            Add(Tab(3), "return Ok(response.Contract);");
            Add(Tab(2), "}", NewLine());
        }

        private void BuildDelete()
        {
            Add(Tab(2), "public async Task<IHttpActionResult> Delete(int id)");
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("await {0}Service.Delete{0}(id);", EntityName));
            Add(Tab(3), "await Task.WhenAll(CurrentSession.CompleteAsync());");
            Add(Tab(3), "return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));");
            Add(Tab(2), "}");
        }
        #endregion
    }
}
