--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2
-- Dumped by pg_dump version 13.2

-- Started on 2021-03-16 06:00:55 EDT

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

SET default_table_access_method = heap;

--
-- TOC entry 200 (class 1259 OID 24620)
-- Name: eligibility_criterion; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.eligibility_criterion (
    nct_id character varying(255) NOT NULL,
    concept_name character varying(255),
    concept_id integer NOT NULL,
    domain_id character varying(255) NOT NULL,
    cat_elig integer NOT NULL,
    lab_elig_min integer,
    lab_elig_max integer,
    CONSTRAINT ck_domain_id CHECK ((((domain_id)::text = 'Measurement'::text) OR ((domain_id)::text = 'Observation'::text) OR ((domain_id)::text = 'Procedure'::text) OR ((domain_id)::text = 'Condition'::text) OR ((domain_id)::text = 'Drug'::text))),
    CONSTRAINT ck_lab_elig_min_max CHECK ((((lab_elig_min IS NULL) AND (lab_elig_max IS NULL)) OR (NOT ((lab_elig_min IS NULL) AND (lab_elig_max IS NULL)))))
);


--
-- TOC entry 3251 (class 0 OID 24620)
-- Dependencies: 200
-- Data for Name: eligibility_criterion; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public.eligibility_criterion (nct_id, concept_name, concept_id, domain_id, cat_elig, lab_elig_min, lab_elig_max) FROM stdin;
NCT00562356	gender	4135376	Observation	1	\N	\N
NCT00562356	alcohol abuse	433753	Condition	1	\N	\N
NCT00562356	anemia	439777	Condition	0	\N	\N
NCT00562356	angina	4324893	Condition	0	\N	\N
NCT00562356	arrhythmia	4068155	Condition	0	\N	\N
NCT00562356	basal skin cancer	4112752	Condition	1	\N	\N
NCT00562356	beta-blocker	4097918	Observation	0	\N	\N
NCT00562356	breast feeding	4289014	Observation	1	\N	\N
NCT00562356	cancer	4194405	Condition	1	\N	\N
NCT00562356	cardiovascular disease	36716864	Condition	0	\N	\N
NCT00562356	cerebrovascular disease	381591	Condition	0	\N	\N
NCT00562356	chronic pancreatitis	195596	Condition	0	\N	\N
NCT00562356	cirrhosis	4252074	Condition	0	\N	\N
NCT00562356	chronic kidney disease	46271022	Condition	0	\N	\N
NCT00562356	coronary artery disease	4168972	Condition	0	\N	\N
NCT00562356	cor bypass surgery	4324192	Observation	0	\N	\N
NCT00562356	diabetes with ketoacidosis	443727	Condition	0	\N	\N
NCT00562356	dialysis	4082257	Procedure	0	\N	\N
NCT00562356	drug abuse	436954	Condition	0	\N	\N
NCT00562356	endocrine disease	4028942	Condition	0	\N	\N
NCT00562356	gastrointestinal disease	4264850	Condition	0	\N	\N
NCT00562356	gastroparesis	195847	Condition	0	\N	\N
NCT00562356	gestational diabetes	4024659	Condition	0	\N	\N
NCT00562356	glucagon	1560278	Drug	0	\N	\N
NCT00562356	heart failure	316139	Condition	0	\N	\N
NCT00562356	hematological disease	36716893	Condition	0	\N	\N
NCT00562356	hepatitis b	4340379	Condition	0	\N	\N
NCT00562356	hepatitis c	192242	Condition	0	\N	\N
NCT00562356	HIV	4013106	Condition	0	\N	\N
NCT00562356	hypertension	320128	Condition	0	\N	\N
NCT00562356	hypoglyemia	24609	Condition	0	\N	\N
NCT00562356	irritable bowel disease	75576	Condition	0	\N	\N
NCT00562356	kidney disease	198124	Condition	1	\N	\N
NCT00562356	kidney transplant	4322471	Procedure	0	\N	\N
NCT00562356	liver disease	4340391	Condition	1	\N	\N
NCT00562356	major surgery	4059344	Observation	0	\N	\N
NCT00562356	metformin	1503297	Drug	0	\N	\N
NCT00562356	myocardial infarction	4329847	Condition	0	\N	\N
NCT00562356	neuropathy	4301699	Condition	0	\N	\N
NCT00562356	panreatitis	4192640	Condition	0	\N	\N
NCT00562356	peripheral artery disease	4354083	Condition	0	\N	\N
NCT00562356	pre-diabetes	37018196	Condition	0	\N	\N
NCT00562356	pregnant	4299535	Condition	1	\N	\N
NCT00562356	proliferative retinopathy	45757798	Condition	0	\N	\N
NCT00562356	pulmonary disease	4196712	Condition	0	\N	\N
NCT00562356	retinopthy	4336003	Condition	0	\N	\N
NCT00562356	smoking	4141787	Observation	0	\N	\N
NCT00562356	stroke	4145413	Observation	0	\N	\N
NCT00562356	substance abuse	4279309	Condition	1	\N	\N
NCT00562356	sulfonylurea	4187003	Observation	0	\N	\N
NCT00562356	surgery	4058419	Observation	0	\N	\N
NCT00562356	thiazolidinediones	4030950	Drug	0	\N	\N
NCT00562356	thyroid cancer	4312667	Condition	0	\N	\N
NCT00562356	transient ischemic attack	4058289	Observation	0	\N	\N
NCT00562356	type 1 diabetes	201254	Condition	0	\N	\N
NCT00562356	weight loss surgery	43021746	Observation	0	\N	\N
NCT00562356	hba1c	4197971	Measurement	1	7	10
NCT00562356	glucose	4149519	Measurement	1	126	2000
NCT00562356	creatanine	4324383	Measurement	1	0	3
NCT00562356	bilirubin	4118986	Measurement	0	0	2000
NCT00562356	LDL	4210878	Measurement	0	0	2000
NCT00562356	AST	4094595	Measurement	1	0	100
NCT00562356	ALT	4095055	Measurement	1	0	130
NCT00562356	HDL	4195503	Measurement	0	0	2000
NCT00562356	hemoglobin	4017624	Measurement	0	0	2000
NCT00562356	triglycerides	4032789	Measurement	0	0	2000
NCT00562356	total cholesterol	4008265	Measurement	0	0	2000
NCT00562356	eGFR	3040084	Measurement	0	0	2000
NCT00562356	age	4265453	Observation	1	18	90
NCT02885496	gender	4135376	Observation	1	\N	\N
NCT02885496	alcohol abuse	433753	Condition	1	\N	\N
NCT02885496	anemia	439777	Condition	0	\N	\N
NCT02885496	ALT	4095055	Measurement	1	0	130
NCT02885496	eGFR	3040084	Measurement	0	0	2000
NCT02885496	age	4265453	Observation	1	18	90
\.


--
-- TOC entry 3120 (class 2606 OID 24629)
-- Name: eligibility_criterion eligibility_criterion_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.eligibility_criterion
    ADD CONSTRAINT eligibility_criterion_pkey PRIMARY KEY (nct_id, concept_id);


-- Completed on 2021-03-16 06:00:55 EDT

--
-- PostgreSQL database dump complete
--

