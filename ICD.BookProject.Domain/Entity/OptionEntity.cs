using System.Collections.Generic;
using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class OptionEntity : BaseEntity<int>
{
    public string Text { get; set; }

    #region Navigation Property

    public int QuestionRef { get; set; }
    public QuestionEntity Question { get; set; }
    public ICollection<ResponseEntity> Responses { get; set; }
    
    
    #endregion
}