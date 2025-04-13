using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class ResponseEntity : BaseEntity<int>
{
    public string Text { get; set; }
    public int? Digit { get; set; }
    
    #region Navigation Property

    public int?  OptionRef { get; set; }
    public OptionEntity Option { get; set; }
    public long UserRef { get; set; }
    public UserEntity User { get; set; }
    public int QuestionRef { get; set; }
    public QuestionEntity Question { get; set; }

    #endregion
}