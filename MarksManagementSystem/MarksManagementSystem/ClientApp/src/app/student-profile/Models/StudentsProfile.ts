
export class StudentMarks {

    public Hallticket: string; 
    public  SubjectName: string;
    public SubjectCode: string;
    public Year: number;
    public Sem: number;
    public Grade: number;
    public GradePoint: number;

}

  export class StudentProfile {
    public StudentMarksByYearList: StudentMarksByYear[];
    public NAAC: number;
    public TotalAverage: number;
    public TotalNoOfBacklogs: number;

}
 export class StudentMarksByYear {
  public StudentMarks: StudentMarks[];
  public Average: number;
  public NoOfBacklogs: number;
}



