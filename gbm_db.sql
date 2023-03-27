--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: category; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.category AS ENUM (
    'Hall',
    'Lane',
    'Studio'
);


ALTER TYPE public.category OWNER TO postgres;

--
-- Name: status; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.status AS ENUM (
    'Hall',
    'Lane',
    'Studio'
);


ALTER TYPE public.status OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: activitys; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.activitys (
    activity_id integer NOT NULL,
    activity_details text,
    participant_limits integer,
    start_time date,
    duration_min double precision,
    activity_owner integer,
    activity_space integer,
    activity_trainer integer,
    activity_equipment integer
);


ALTER TABLE public.activitys OWNER TO postgres;

--
-- Name: activitys_activity_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.activitys_activity_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.activitys_activity_id_seq OWNER TO postgres;

--
-- Name: activitys_activity_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.activitys_activity_id_seq OWNED BY public.activitys.activity_id;


--
-- Name: equipments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.equipments (
    equipment_id integer NOT NULL,
    category integer,
    equipment_name text
);


ALTER TABLE public.equipments OWNER TO postgres;

--
-- Name: equipments_equipment_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.equipments_equipment_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.equipments_equipment_id_seq OWNER TO postgres;

--
-- Name: equipments_equipment_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.equipments_equipment_id_seq OWNED BY public.equipments.equipment_id;


--
-- Name: spaces; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.spaces (
    space_id integer NOT NULL,
    category integer,
    space_name text
);


ALTER TABLE public.spaces OWNER TO postgres;

--
-- Name: spaces_space_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.spaces_space_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.spaces_space_id_seq OWNER TO postgres;

--
-- Name: spaces_space_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.spaces_space_id_seq OWNED BY public.spaces.space_id;


--
-- Name: trainers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.trainers (
    trainer_id integer NOT NULL,
    category integer,
    trainer_name text
);


ALTER TABLE public.trainers OWNER TO postgres;

--
-- Name: trainers_trainer_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.trainers_trainer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.trainers_trainer_id_seq OWNER TO postgres;

--
-- Name: trainers_trainer_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.trainers_trainer_id_seq OWNED BY public.trainers.trainer_id;


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    user_name text,
    user_phone text,
    user_email text,
    user_status text,
    user_uniqueid text
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_user_id_seq OWNER TO postgres;

--
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- Name: activitys activity_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.activitys ALTER COLUMN activity_id SET DEFAULT nextval('public.activitys_activity_id_seq'::regclass);


--
-- Name: equipments equipment_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.equipments ALTER COLUMN equipment_id SET DEFAULT nextval('public.equipments_equipment_id_seq'::regclass);


--
-- Name: spaces space_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.spaces ALTER COLUMN space_id SET DEFAULT nextval('public.spaces_space_id_seq'::regclass);


--
-- Name: trainers trainer_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trainers ALTER COLUMN trainer_id SET DEFAULT nextval('public.trainers_trainer_id_seq'::regclass);


--
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- Data for Name: activitys; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.activitys (activity_id, activity_details, participant_limits, start_time, duration_min, activity_owner, activity_space, activity_trainer, activity_equipment) FROM stdin;
1	ZumbaDelux	10	2023-03-23	30	2	1	1	1
\.


--
-- Data for Name: equipments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.equipments (equipment_id, category, equipment_name) FROM stdin;
1	0	Hantlar
2	1	Rack
\.


--
-- Data for Name: spaces; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.spaces (space_id, category, space_name) FROM stdin;
1	0	Gym
2	1	Spinning
3	2	Open Box
\.


--
-- Data for Name: trainers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.trainers (trainer_id, category, trainer_name) FROM stdin;
1	0	Sara
2	1	Boris
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (user_id, user_name, user_phone, user_email, user_status, user_uniqueid) FROM stdin;
1	Bosse	0703825461	Boss@king.se	member	200
2	Doris	0705896324	FlåsDoris@cykel.se	staff	100
3	Bengt	0105682345	Byggare@Gains.org	service	300
\.


--
-- Name: activitys_activity_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.activitys_activity_id_seq', 1, true);


--
-- Name: equipments_equipment_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.equipments_equipment_id_seq', 2, true);


--
-- Name: spaces_space_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.spaces_space_id_seq', 3, true);


--
-- Name: trainers_trainer_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.trainers_trainer_id_seq', 2, true);


--
-- Name: users_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_user_id_seq', 3, true);


--
-- Name: activitys activitys_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.activitys
    ADD CONSTRAINT activitys_pkey PRIMARY KEY (activity_id);


--
-- Name: equipments equipments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.equipments
    ADD CONSTRAINT equipments_pkey PRIMARY KEY (equipment_id);


--
-- Name: spaces spaces_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.spaces
    ADD CONSTRAINT spaces_pkey PRIMARY KEY (space_id);


--
-- Name: trainers trainers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trainers
    ADD CONSTRAINT trainers_pkey PRIMARY KEY (trainer_id);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- PostgreSQL database dump complete
--

