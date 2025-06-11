using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class ChangeProblemLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Problem> problemsRepository;

        private Problem problem;
        public Problem Problem
        {
            get { return problem; }
            set { problem = value; OnPropertyChanged(nameof(Problem)); }
        }

        public ChangeProblemLogic(IRepository<Problem> problemsRepository)
        {
            this.problemsRepository = problemsRepository;
        }

        public ChangeProblemLogic()
        {
            this.problemsRepository = new ProblemsRepository();
        }


        public void changeProblem(Problem problem)
        {
            problemsRepository.Update(problem);
        }


        public void deleteProblem(Problem problem)
        {
            problemsRepository.Delete(problem);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
