# Contact-zoo-at-home

## Description

  You can take a book from library or a film from Netflix, but I've always wondered, why can't I get a pet for a short period of time, like few hours or a day, just to play with it. We all love pets, but not all of us have enough time to take care of it. With this application you can get a kitten or a puppy or, perhaps, spider for a limited period, and have a good time with it.

  You can choose pet/pets you like, and they will be delivered just to your place. After that you can pet it, hug it or do whatever you want (within user licence and additional rulles set by owners).

  Some pets may be even purchused after your play session, if this option is set in it's profile, in case you will fill attached to it ^^.

  Pets may be chosen from the veriaty of our partners: zoo-shops, or animal-shelters, ot even individual owners.

## Project

represents general idea of the project.

### General
- Project looks like a sort of delivery service where you choose pets you like.
- Comments and raiting and all needed information of the pet may be watched in it's profile.
- Only registered users can make orders, unsighned users still may watch full list of animals, and animal profiles.
- After making an order, user is waiting to be contact by all pets owners and discussing details, after that an initial payment should be made.

### Apperence
1. main pages
   - About
   - List of pets (Rename later)
   - our partners (list of all zoo-shops or animal-shelters with their local profiles)
   - Contact Us
2. sub pages
   - user profile + settings
   - Animal profile + settings
   - company profile + maybe settings
   - pet cart/basket (корзинка з тваринками, віііі)
   - Sign in / sign up

### Behind scene logic

One orders pets. One being contact. Both(or more) parties agree. On the date, all pets come with their owners or other human-friend-representitives[^1]. If something indangers pets during they stay, the contract is automaticly declined.

In case one of the pets can't be a part of the contract anymore(before delivery), user gets full refund for this pet, or for all list of pets from this company.

### Payment logic

1. User pays after all parties contacted with him, and everyone agrees to the contract.
2. User can get full refund if there is one or more days before contact left.
   2.a. If there is less then 24-hours left, that he may get half of his sum.
3. Pet owner can decline contract in any time before delivery, and user gets full refund.
4. If there are any controversyes on the contract, money may be frozen untill third-party investigation.

### Pet profile apperence

- Name
- Species
- Link to owner
- raiting
- short/long description
- general info about pet (size, color, height, etc.)
- price + price per hour
- comments from others

### Other

## Use cases

Represents action only within app.

1. Unsigned User:
  - Watch pets;
  - Watch comments;
  - Watch companies etc;
  - Form PetCart, if possible, but no ordering.
  
  1.1. Signed User:
    - All above;
    - Leave comments.

2. CustomerUser:
  - Can do everything Signed User can;
  - But also make orders;
  - Look through his current orders;
  - Decline current order (with some refund logic);
  - ReAccept order if some changes were made;
  - And rate pets after order is finished.

3. PetOwner:
  - Can do everething Signed User can;
  - Add new of his pets;
  - Look through new contracts;
  - Look through active contracts;
  - Look through closed contracts;
  - Accept new contracts making them active;
  - Modify new or active contracts;
  - Remove pets if they were bought or smt;
  - Set his pets as imposible to order.
  - Sign unregistered user as his pet representative in contract.

4. CompanyAsUser:
  - Can do everething Signed User can;
  - Can do everething PetOwner can;
  - Add new employees to the app.

5. PetRepresentative:
  - Can do everething Signed User can;
  - Can look through contracts that he is involved in.

6. SiteAdmin:
  - Register new companies;
  - Freeze contracts if needed;
  - Freeze payment if needed (actualy == freezing contracts).

## ToDo List
- [ ] ToDo ToDo List
