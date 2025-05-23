using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand UpdateProblemCommand { get; set; }
        public ICommand DeleteProblemCommand { get; set; }
        public ChangeProblemLogic(IRepository<Problem> problemsRepository)
        {
            this.problemsRepository = problemsRepository;
            UpdateProblemCommand = new RelayCommand(changeProblem);
            DeleteProblemCommand = new RelayCommand(deleteProblem);
        }

        private void changeProblem(object param)
        {
            problemsRepository.Update(problem);
        }

        private void deleteProblem(object param)
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
