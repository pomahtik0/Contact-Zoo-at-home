﻿namespace Contact_zoo_at_home.Shared.Dto.Pet
{
    public class DisplayPetsShortDto
    {
        public IList<DisplayPetShortDto> Pets { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
