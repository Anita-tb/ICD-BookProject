using System.Collections.Generic;
using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class QuestionEntity : BaseEntity<int>
{
    public string Text { get; set; }
    public string Type { get; set; }

    #region Navigation Property

    public int QuestionnaireRef { get; set; }
    public QuestionnaireEntity Questionnaire { get; set; }
    public ICollection<OptionEntity> Options { get; set; }
    public ICollection<ResponseEntity> Responses { get; set; }
    
    #endregion
}