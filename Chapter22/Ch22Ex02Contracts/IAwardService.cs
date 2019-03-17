using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Ch22Ex02Contracts
{
    // This service requires state, hence the SessionMode.Required
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAwardService
    {
        // This is the operation that sets the state, thus IsInitiating is true.
        // It doesn't return anything, therefore IsOneWay is true.
        [OperationContract(IsOneWay = true, IsInitiating = true)]
        void SetPassMark(int passMark);

        [OperationContract]
        Person[] GetAwardedPeople(Person[] peopleToTest);
    }
}
