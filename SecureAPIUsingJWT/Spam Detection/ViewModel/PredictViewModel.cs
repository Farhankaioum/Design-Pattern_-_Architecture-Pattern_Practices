using Spam_Detection.Data;
using Spam_Detection.ML_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spam_Detection.ViewModel
{
    public class PredictViewModel
    {

        public bool IsTrue(SpamInput input)
        {
            var model = new SpamDetectionMLModel();
            model.Build();
            model.Train();
            var result = model.Predict(input);

            return Convert.ToBoolean(result.isSpam);
        }
    }
}
