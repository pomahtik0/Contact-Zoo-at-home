##Users:
**User** -- base word for the user of application. **User** may be either **Customer** or **Pet Owner** and **Contractor**. One cannot perform both roles in the same time.
**Customer** -- **User** that creates contracts to get **Pets** for limited time.
**Contractor** -- **User** who allows to use their **Pets**. Acts as one who **Performs Contract**.
**Pet Owner** -- someone who owns a **Pet** or **Pets** and wants them to be a part of **Contract**. May be human or company or whatever. Acts as a **Contractor**.
**Representative** -- third party non-user individual, who acts as representative of a **Company** in the **Contract**.
**Company** -- is a **User**. May be a large group of people. Is a **Contractor**, but uses **Representatives** to **Perform Contract**.
**Individual Owner** -- one individual, who is **Pet Owner**.

##Contracts
**Contract** -- is a negotiation beetween **Customer** and **Contractor**. **Contractor** is supposed to provide **Customer** with specified **Pets** on a specified date in a specified location. **Customer** pays money for services provided.
**Base Contract** -- is a one day **Contract** with some basic services. Applies between **Customer** and **Contractor**.
**Premium Contract** -- is actualy like **Base Contract**, but may be provided for more then one day, for example a week.
**Poly Contract** -- if **Customer** wants to create a **Contract** with more then one **Contractor**, but want it to be one **Contract** rather then few **Base Contracts**, then **Poly Contract** is applied. If one of the **Contractors** **Declines Contract**, then it is considered that all **Contractors** declined it.
**Create Contract** -- action perfermed by a **Customer** to make **Contractor** provide it's pets. After **Create Contract** action both parties can either **Decline Contract** or to **Perform** it.
**Decline Contract** -- if contract cant be **Permormed** by **Contractor** then it must be declined by **Contractor**. Contract declined by **Contractor** should give **Customer** full **Refund**. If contract is declined by the **Customer** refund logic may differ.
**Perferm Contract** -- means to make all actions asigned in a contract for the **Customer** by a **Contractor**.
**Money** -- Payment logic and all terms connected, are described in the other part of documentation.

##Other
**Pets** -- non-human living beings owned by **Pet Owners**, and may be a part of a **Contract**.
**Comments** -- short reviews for a **User** or a **Pet** left by other **Users**.
**Rating** -- mark form 0 to 5 for a **User** or a **Pet** left by other **Users**.
**Notifications** -- short messages to come within application or by 3-rd partie methods to give **User** needed information.