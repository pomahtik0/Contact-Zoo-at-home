﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class FullDisplayPetDto : ShortDisplayPetDto
    {
        public byte[][] Images { get; set; }
        public string Description {  get; set; }
        public IEnumerable<PetCommentsDto> Comments { get; set; }
    }
}
