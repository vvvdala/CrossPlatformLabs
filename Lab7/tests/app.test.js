import { expect } from 'chai';
import request from 'supertest';

const baseUrl = 'http://localhost:5047/api';

describe('Booking tests', ()=> {
    it('GET /Booking - повино поверне всі бронювання', async() => {
        const res = await request(baseUrl).get('/Booking');
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('array');
    });

    it('GET /Booking - повино поверне конкретне бронювання', async() => {
        const id = 4;
        const res = await request(baseUrl).get(`/Booking/${id}`);
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('object')
        expect(res.body).to.have.property('bookingId');
        expect(res.body.bookingId).to.equal(id);
    });

});

describe('Booking search tests', ()=> {
    it('GET /Booking - повино повернути бронювання за діапазоном дат', async() => {
        const dateFrom = '2024-11-06';
        const dateTo = '2024-11-09';
        const res = await request(baseUrl).get('/Booking/search').query({dateFrom,dateTo});
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('array');
        res.body.forEach(booking => {
            expect(new Date(booking.dateFrom).to.be.greaterThanOrEqual(new Date(dateFrom)));
            expect(new Date(booking.dateTo.lessThanOrEqual(new Date(dateTo))));
        });
    });

    it('GET /Booking - повино повернути бронювання за статусами', async() => {
        const statusCodes = ['NEW', 'CON', 'CNC'];  
        const res = await request(baseUrl).get('/Booking/search').query({ statusCodes });
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('array');
        res.body.forEach(booking => {
            expect(statusCodes).to.include(booking.statusCode);
        });
    });

    it('GET /Booking - повино повернути бронювання за початковим номером реєстрації автомобіля', async() => {
        const startVehicleReg = 'V';
        const res = await request(baseUrl).get('/Booking/search').query({startVehicleReg});
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('array');
        res.body.forEach(booking => {
            expect(booking.vehicleRegNumber).to.match(new RegExp(`^${startVehicleReg}`));
        });
    });

    it('GET /Booking - повино повернути бронювання за кінцевим номером реєстрації автомобіля', async() => {
        const endVehicleReg = '1 ';
        const res = await request(baseUrl).get('/Booking/search').query({endVehicleReg});
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('array');
        res.body.forEach(booking => {
            expect(booking.vehicleRegNumber).to.match(new RegExp(`${endVehicleReg}$`));
        });
    });



});

describe('Vehcile tests', ()=> {
    it('GET /Vehicle - повино повернути всі транспортні засоби', async() => {
        const res = await request(baseUrl).get(`/Vehicle`);
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('array');
    });

    it('GET /Vehicle - повино повернути конкретний транспортний засіб', async() => {
        const regNumb = 'V001 ';
        const res = await request(baseUrl).get(`/Vehicle/${regNumb}`);
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('object')
        expect(res.body).to.have.property('regNumber');
        expect(res.body.regNumber).to.equal(regNumb);
    });
});

describe('BookingStatus tests', ()=> {
    it('GET /BookingStatus - повино повернути всі статуси бронювання', async() => {
        const res = await request(baseUrl).get(`/v1/BookingStatus`);
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('array');
    });

    it('GET /BookingStatus - повино повернути конкретний статус бронювання', async() => {
        const id = 'CMP';
        const res = await request(baseUrl).get(`/v1/BookingStatus/${id}`);
        expect(res.status).to.equal(200);
        expect(res.body).to.be.an('object')
        expect(res.body).to.have.property('bookingStatusCode');
        expect(res.body.bookingStatusCode).to.equal(id);
    });
});